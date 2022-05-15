using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;
using MinTur.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using MinTur.Models.In;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class ChargingPointControllerTest
    {
        private Mock<IChargingPointManager> _chargingPointManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _chargingPointManagerMock = new Mock<IChargingPointManager>(MockBehavior.Strict);
        }
        #endregion



        [TestMethod]
        public void GetSpecificChargingPoint()
        {
            ChargingPoint returnedChargingPoint = CreateChargingPoint();

            _chargingPointManagerMock.Setup(c => c.GetChargingPointById(It.IsAny<int>())).Returns(returnedChargingPoint);
            ChargingPointController chargingPointController = new ChargingPointController(_chargingPointManagerMock.Object);

            IActionResult result = chargingPointController.GetSpecificChargingPoint(returnedChargingPoint.Id);
            OkObjectResult okResult = result as OkObjectResult;
            ChargingPointBasicInfoModel responseModel = okResult.Value as ChargingPointBasicInfoModel;

            _chargingPointManagerMock.VerifyAll();
            Assert.IsTrue(responseModel.Equals(new ChargingPointBasicInfoModel(returnedChargingPoint)));
        }

        [TestMethod]
        public void CreateChargingPointCreatedAtTest()
        {
            ChargingPointIntentModel chargingPointIntentModel = CreateChargingPointIntentModel();
            ChargingPoint expectedChargingPoint = CreateChargingPoint();

            _chargingPointManagerMock.Setup(t => t.RegisterChargingPoint(It.IsAny<ChargingPoint>())).Returns(expectedChargingPoint);
            ChargingPointController chargingPointController = new ChargingPointController(_chargingPointManagerMock.Object);

            IActionResult result = chargingPointController.CreateChargingPoint(chargingPointIntentModel);
            CreatedResult createdResult = result as CreatedResult;

            _chargingPointManagerMock.VerifyAll();
            Assert.IsTrue(createdResult.StatusCode == StatusCodes.Status201Created);
            Assert.AreEqual(new ChargingPointBasicInfoModel(expectedChargingPoint), createdResult.Value);
        }

        #region Helpers
        private ChargingPointIntentModel CreateChargingPointIntentModel()
        {
            return new ChargingPointIntentModel()
            {
                Name = "Punta del este",
                Description = "Descripcion...",
                RegionId = 3,
                Address = new string('a', 20),
            };
        }
        private ChargingPoint CreateChargingPoint()
        {
            return new ChargingPoint()
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
                Address = new string('a', 20),
            };
        }
        #endregion
    }
}
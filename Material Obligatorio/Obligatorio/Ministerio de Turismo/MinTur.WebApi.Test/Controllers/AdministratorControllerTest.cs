using System.Collections.Generic;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using MinTur.Models.Out;
using Moq;
using MinTur.WebApi.Controllers;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class AdministratorControllerTest
    {
        private List<Administrator> _administrators;
        private List<AdministratorBasicInfoModel> _administratorBasicInfoModels;
        private Mock<IAdministratorManager> _administratorManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _administrators = new List<Administrator>();
            _administratorBasicInfoModels = new List<AdministratorBasicInfoModel>();
            _administratorManagerMock = new Mock<IAdministratorManager>(MockBehavior.Strict);

            LoadAdministrators();
            LoadAdministratorBasicInfoModels();
        }

        private void LoadAdministrators()
        {
            Administrator administrator1 = new Administrator() { Id = 1, Email = "email@email.com", Password = "password" };
            Administrator administrator2 = new Administrator() { Id = 2, Email = "email2@email.com", Password = "password2" };
            _administrators.Add(administrator1);
            _administrators.Add(administrator2);
        }


        private void LoadAdministratorBasicInfoModels()
        {
            foreach (Administrator administrator in _administrators)
            {
                _administratorBasicInfoModels.Add(new AdministratorBasicInfoModel(administrator));
            }
        }

        #endregion

        [TestMethod]
        public void GetAllAdministratorsOkTest()
        {
            _administratorManagerMock.Setup(c => c.GetAllAdministrators()).Returns(_administrators);
            AdministratorController administratorController = new AdministratorController(_administratorManagerMock.Object);

            IActionResult result = administratorController.GetAll();
            OkObjectResult okResult = result as OkObjectResult;
            List<AdministratorBasicInfoModel> responseModel = okResult.Value as List<AdministratorBasicInfoModel>;

            _administratorManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(responseModel, _administratorBasicInfoModels);
        }

        [TestMethod]
        public void DeleteAdminOkTest()
        {
            int adminIdForDeletition = 4;
            string succesfulDeletitionMessage = new { ResultMessage = "Administrator " + adminIdForDeletition + " succesfuly deleted" }.ToString();

            _administratorManagerMock.Setup(c => c.DeleteAdministratorById(adminIdForDeletition));
            AdministratorController administratorController = new AdministratorController(_administratorManagerMock.Object);

            IActionResult result = administratorController.DeleteAdministrator(adminIdForDeletition);
            OkObjectResult okResult = result as OkObjectResult;
            string retrievedResultMessage = okResult.Value.ToString();

            _administratorManagerMock.VerifyAll();
            Assert.AreEqual(succesfulDeletitionMessage, retrievedResultMessage);
        }

        [TestMethod]
        public void CreateAdministratorCreatedTest()
        {
            AdministratorIntentModel administratorIntentModel = CreateAdministratorIntentModel();
            Administrator createdAdministrator = CreateAdministrator();

            _administratorManagerMock.Setup(a => a.RegisterAdministrator(It.IsAny<Administrator>())).Returns(createdAdministrator);
            AdministratorController administratorController = new AdministratorController(_administratorManagerMock.Object);

            IActionResult result = administratorController.CreateAdministrator(administratorIntentModel);
            CreatedResult createdResult = result as CreatedResult;

            _administratorManagerMock.VerifyAll();
            Assert.IsTrue(createdResult.Value.Equals(new AdministratorBasicInfoModel(createdAdministrator)));
        }

        [TestMethod]
        public void UpdateAdministratrorOkTest()
        {
            AdministratorIntentModel administratorIntentModel = CreateAdministratorIntentModel();
            Administrator updatedAdministrator = administratorIntentModel.ToEntity();
            updatedAdministrator.Id = 1;

            _administratorManagerMock.Setup(a => a.UpdateAdministrator(It.IsAny<Administrator>())).Returns(updatedAdministrator);
            AdministratorController administratorController = new AdministratorController(_administratorManagerMock.Object);

            IActionResult result = administratorController.UpdateAdministrator(updatedAdministrator.Id, administratorIntentModel);
            OkObjectResult okResult = result as OkObjectResult;

            _administratorManagerMock.VerifyAll();
            Assert.IsTrue(okResult.Value.Equals(new AdministratorBasicInfoModel(updatedAdministrator)));
        }

        [TestMethod]
        public void GetAdministratorByIdOkTest() 
        {
            int administratorId = 4;
            Administrator expectedAdministrator = CreateAdministratorWithSpecificId(administratorId);

            _administratorManagerMock.Setup(a => a.GetAdministratorById(administratorId)).Returns(expectedAdministrator);
            AdministratorController administratorController = new AdministratorController(_administratorManagerMock.Object);

            IActionResult result = administratorController.GetSpecificAdministrator(administratorId);
            OkObjectResult okResult = result as OkObjectResult;

            _administratorManagerMock.VerifyAll();
            Assert.AreEqual(new AdministratorBasicInfoModel(expectedAdministrator), okResult.Value);
        }

        #region Helpers
        public AdministratorIntentModel CreateAdministratorIntentModel()
        {
            return new AdministratorIntentModel()
            {
                Email = "email@email.com",
                Password = "password"
            };
        }

        public Administrator CreateAdministrator()
        {
            return new Administrator()
            {
                Id = 1,
                Email = "email@email.com",
                Password = "password"
            };
        }
        public Administrator CreateAdministratorWithSpecificId(int administratorId)
        {
            return new Administrator()
            {
                Id = administratorId,
                Email = "email@email.com",
                Password = "password"
            };
        }
        #endregion
    }
}
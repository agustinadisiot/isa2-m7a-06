using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using Moq;
using MinTur.BusinessLogicInterface.Security;
using System;
using MinTur.WebApi.Controllers;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class SessionControllerTest
    {
        private Mock<IAuthenticationManager> _authenticationManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp() 
        {
            _authenticationManagerMock = new Mock<IAuthenticationManager>(MockBehavior.Strict);
        }
        #endregion

        [TestMethod]
        public void LoginOkTest() 
        {
            AdministratorIntentModel administratorIntentModel = CreateAdministratorIntentModel();
            Guid expectedGuid = Guid.NewGuid();
            _authenticationManagerMock.Setup(a => a.Login(administratorIntentModel.ToEntity())).Returns(expectedGuid);

            SessionController sessionController = new SessionController(_authenticationManagerMock.Object);
            IActionResult result = sessionController.Login(administratorIntentModel);
            OkObjectResult okResult = result as OkObjectResult;

            _authenticationManagerMock.VerifyAll();
            Assert.AreEqual(expectedGuid, okResult.Value);
        }

        #region Helpers
        private AdministratorIntentModel CreateAdministratorIntentModel() 
        {
            return new AdministratorIntentModel()
            {
                Email = "juan@gmail.com",
                Password = "juan123"
            };
        }
        #endregion
    }
}

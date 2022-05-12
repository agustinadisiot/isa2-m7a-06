using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogic.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.Test.ResourceManagers
{
    [TestClass]
    public class AdministratorManagerTest
    {
        private List<Administrator> _administrators;
        private Mock<IRepositoryFacade> _repositoryFacadeMock;
        private Mock<Administrator> _administratorMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _administrators = new List<Administrator>();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);
            _administratorMock = new Mock<Administrator>(MockBehavior.Strict);

            LoadAdministrators();
        }

        private void LoadAdministrators()
        {
            Administrator administrator1 = new Administrator() { Id = 1, Email = "email@email.com", Password = "password" };
            Administrator administrator2 = new Administrator() { Id = 2, Email = "email2@email.com", Password = "password2" };
            _administrators.Add(administrator1);
            _administrators.Add(administrator2);
        }

        #endregion


        [TestMethod]
        public void GetAllAdministratorsReturnsAsExpected()
        {
            _repositoryFacadeMock.Setup(r => r.GetAllAdministrators()).Returns(_administrators);

            AdministratorManager administratorManager = new AdministratorManager(_repositoryFacadeMock.Object);
            List<Administrator> retrievedAdministrators = administratorManager.GetAllAdministrators();

            _repositoryFacadeMock.VerifyAll();
            CollectionAssert.AreEquivalent(retrievedAdministrators, _administrators);
        }

        [TestMethod]
        public void DeleteAdministratorByIdDoesAsExpected()
        {
            int administratorId = 7;
            _repositoryFacadeMock.Setup(r => r.DeleteAdministratorById(administratorId));

            AdministratorManager administratorManager = new AdministratorManager(_repositoryFacadeMock.Object);
            administratorManager.DeleteAdministratorById(administratorId);

            _repositoryFacadeMock.VerifyAll();
        }

        [TestMethod]
        public void RegisterAdministratorReturnsAsExpected()
        {
            int administratorId = 6;
            Administrator createdAdministrator = CreateAdministratorWithSpecificId(administratorId);

            Mock<Administrator> administratorMock = new Mock<Administrator>(MockBehavior.Strict);
            administratorMock.Setup(r => r.ValidOrFail());

            _repositoryFacadeMock.Setup(r => r.StoreAdministrator(administratorMock.Object)).Returns(administratorId);
            _repositoryFacadeMock.Setup(r => r.GetAdministratorById(administratorId)).Returns(createdAdministrator);

            AdministratorManager administratorManager = new AdministratorManager(_repositoryFacadeMock.Object);
            Administrator retrievedAdministrator = administratorManager.RegisterAdministrator(administratorMock.Object);

            _repositoryFacadeMock.VerifyAll();
            administratorMock.VerifyAll();
            Assert.AreEqual(createdAdministrator, retrievedAdministrator);
        }


        [TestMethod]
        public void UpdateAdministratorReturnsAsExpected()
        {
            int administratorId = 6;
            Administrator expectedAdministrator = new Administrator()
            {
                Id = administratorId,
                Email = "test@gmail.com",
                Password = "Password2"
            };

            _administratorMock.SetupAllProperties();
            _administratorMock.Setup(r => r.ValidOrFail());
            _administratorMock.Object.Id = administratorId;

            _repositoryFacadeMock.Setup(r => r.UpdateAdministrator(_administratorMock.Object));
            _repositoryFacadeMock.Setup(r => r.GetAdministratorById(administratorId)).Returns(expectedAdministrator);

            AdministratorManager administratorManager = new AdministratorManager(_repositoryFacadeMock.Object);
            Administrator updatedAdministrator = administratorManager.UpdateAdministrator(_administratorMock.Object);

            _repositoryFacadeMock.VerifyAll();
            _administratorMock.VerifyAll();
            Assert.IsTrue(updatedAdministrator.Equals(expectedAdministrator));
        }

        [TestMethod]
        public void GetAdministratorByIdReturnsAsExpected() 
        {
            int administratorId = 6;
            Administrator expectedAdministrator = new Administrator()
            {
                Id = administratorId,
                Email = "test@gmail.com",
                Password = "Password2"
            };
            _repositoryFacadeMock.Setup(r => r.GetAdministratorById(administratorId)).Returns(expectedAdministrator);

            AdministratorManager administratorManager = new AdministratorManager(_repositoryFacadeMock.Object);
            Administrator retrievedAdministrator = administratorManager.GetAdministratorById(administratorId);

            _repositoryFacadeMock.VerifyAll();
            Assert.AreEqual(expectedAdministrator, retrievedAdministrator);
        }

        #region Helpers

        public Administrator CreateAdministratorWithSpecificId(int id)
        {
            return new Administrator()
            {
                Id = id,
                Email = "email@email.com",
                Password = "password"
            };
        }

        #endregion
    }
}
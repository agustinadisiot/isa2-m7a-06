using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogic.Security;
using System;

namespace MinTur.BusinessLogic.Test.Security
{
    [TestClass]
    public class AuthenticationManagerTest
    {
        [TestMethod]
        public void IsTokenValidReturnsTrue() 
        {
            AuthorizationToken authenticationToken = new AuthorizationToken()
            {
                Administrator = new Administrator() { Id = 1, Email = "email@email.com", Password = "password" }
            };
            Mock<IRepositoryFacade> repositoryMock = new Mock<IRepositoryFacade>();
            repositoryMock.Setup(r => r.GetAuthenticationTokenById(It.IsAny<Guid>())).Returns(authenticationToken);
            AuthenticationManager authenticationManager = new AuthenticationManager(repositoryMock.Object);

            bool response = authenticationManager.IsTokenValid(authenticationToken.Id);
            repositoryMock.VerifyAll();
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void IsTokenValidReturnsFalse()
        {
            AuthorizationToken authenticationToken = new AuthorizationToken()
            {
                Administrator = new Administrator() { Id = 1, Email = "email@email.com", Password = "password" }
            };
            Mock<IRepositoryFacade> repositoryMock = new Mock<IRepositoryFacade>();
            repositoryMock.Setup(r => r.GetAuthenticationTokenById(It.IsAny<Guid>())).Throws(new ResourceNotFoundException(""));
            AuthenticationManager authenticationManager = new AuthenticationManager(repositoryMock.Object);

            bool response = authenticationManager.IsTokenValid(authenticationToken.Id);
            repositoryMock.VerifyAll();
            Assert.IsFalse(response);
        }

        [TestMethod]
        public void LoginReturnsNewToken() 
        {
            Guid expectedGuid = Guid.NewGuid();

            Mock<IRepositoryFacade> repositoryMock = new Mock<IRepositoryFacade>();
            Mock<Administrator> administratorMock = new Mock<Administrator>();
            administratorMock.Setup(a => a.ValidOrFail());
            repositoryMock.Setup(r => r.CreateNewAuthorizationTokenFor(administratorMock.Object)).Returns(expectedGuid);
            AuthenticationManager authenticationManager = new AuthenticationManager(repositoryMock.Object);

            Guid retrievedGuid = authenticationManager.Login(administratorMock.Object);
            repositoryMock.VerifyAll();
            Assert.AreEqual(expectedGuid, retrievedGuid);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void LoginWrongAdministratorCredentials()
        {
            Mock<IRepositoryFacade> repositoryMock = new Mock<IRepositoryFacade>();
            Mock<Administrator> administratorMock = new Mock<Administrator>();
            administratorMock.Setup(a => a.ValidOrFail()).Throws(new InvalidRequestDataException(""));
            AuthenticationManager authenticationManager = new AuthenticationManager(repositoryMock.Object);

            Guid retrievedGuid = authenticationManager.Login(administratorMock.Object);
            administratorMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void LogiNonExistentAdministratorCredentials()
        {
            Mock<IRepositoryFacade> repositoryMock = new Mock<IRepositoryFacade>();
            Mock<Administrator> administratorMock = new Mock<Administrator>();
            administratorMock.Setup(a => a.ValidOrFail());
            repositoryMock.Setup(r => r.CreateNewAuthorizationTokenFor(administratorMock.Object)).
                Throws(new ResourceNotFoundException(""));
            AuthenticationManager authenticationManager = new AuthenticationManager(repositoryMock.Object);

            Guid retrievedGuid = authenticationManager.Login(administratorMock.Object);
            administratorMock.VerifyAll();
            repositoryMock.VerifyAll();
        }
    }
}

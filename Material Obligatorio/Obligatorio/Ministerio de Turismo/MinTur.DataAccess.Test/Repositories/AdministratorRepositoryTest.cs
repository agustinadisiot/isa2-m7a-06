using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MinTur.DataAccess.Contexts;
using MinTur.Domain.BusinessEntities;
using MinTur.DataAccess.Repositories;
using MinTur.Exceptions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class AdministratorRepositoryTest
    {
        private AdministratorRepository _repository;

        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new AdministratorRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllAdministratorsOnEmptyRepository()
        {
            List<Administrator> expectedAdministrators = new List<Administrator>();
            List<Administrator> retrievedAdministrators = _repository.GetAllAdministrators();

            CollectionAssert.AreEquivalent(expectedAdministrators, retrievedAdministrators);
        }

        [TestMethod]
        public void GetAllAdministratorsReturnsAsExpected()
        {
            List<Administrator> expectedAdministrators = new List<Administrator>();
            LoadAdministrators(expectedAdministrators);

            List<Administrator> retrievedAdministrators = _repository.GetAllAdministrators();
            CollectionAssert.AreEquivalent(expectedAdministrators, retrievedAdministrators);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteAdministratorWhichDoesntExist()
        {
            _repository.DeleteAdministratorById(-4);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteTheLastAdministratorOnDb()
        {
            int administratorId = 9;
            Administrator administrator = CreateAdministratorWithSpecificId(administratorId);
            InsertAdministratorIntoDb(administrator);

            _repository.DeleteAdministratorById(administratorId);
        }

        [TestMethod]
        public void DeleteAdministratorRemovesAdminAndTokensFromDb()
        {
            int oldAdministratorId = 12;
            int administratorId = 7;
            Administrator oldAdministrator = CreateAdministratorWithSpecificId(oldAdministratorId);
            Administrator administrator = CreateAdministratorWithSpecificId(administratorId);
            AuthorizationToken authorizationToken = CreateAuthorizationTokenForSpecificAdmin(administrator);
            InsertAdministratorIntoDb(oldAdministrator);
            InsertAdministratorIntoDb(administrator);
            InsertAuthorizationTokenIntoDb(authorizationToken);

            _repository.DeleteAdministratorById(administratorId);
            Administrator retrievedAdministrator = _context.Set<Administrator>().Where(a => a.Id == administratorId).FirstOrDefault();
            AuthorizationToken retrievedAuthorizationToken = _context.Set<AuthorizationToken>().Where(at => at.Administrator.Equals(administrator))
                .FirstOrDefault();

            Assert.IsNull(retrievedAdministrator);
            Assert.IsNull(retrievedAuthorizationToken);
        }

        [TestMethod]
        public void StoreAdministratorReturnsAsExpected()
        {
            Administrator administrator = CreateAdministratorWithSpecificId(0);
            int newAdministratorId = _repository.StoreAdministrator(administrator);

            Assert.AreEqual(administrator.Id, newAdministratorId);
            Assert.IsNotNull(_context.Administrators.Where(a => a.Id == newAdministratorId).FirstOrDefault());
        }

        [TestMethod]
        public void GetAdministratorByIdReturnsAsExpected() 
        {
            int administratorId = 2;
            Administrator administrator = CreateAdministratorWithSpecificId(administratorId);
            InsertAdministratorIntoDb(administrator);

            Administrator retrievedAdministrator = _repository.GetAdministratorById(administratorId);
            Assert.AreEqual(administrator, retrievedAdministrator);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetAdministratorByIdWhichDoesntExist()
        {
            _repository.GetAdministratorById(-8);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StorAdministratorWhichEmailAlreadyExist()
        {

            int administratorId = 7;
            Administrator administrator = CreateAdministratorWithSpecificId(administratorId);
            InsertAdministratorIntoDb(administrator);

            _repository.StoreAdministrator(administrator);
        }

        [TestMethod]
        public void UpdateAdministratorUpdatesDb()
        {
            int administratorId = 7;
            Administrator administrator = CreateAdministratorWithSpecificId(administratorId);
            Administrator newAdministrator = new Administrator()
            {
                Id = administratorId,
                Email = "test@gmail.com",
                Password = "Password2"
            };
            InsertAdministratorIntoDb(administrator);

            _repository.UpdateAdministrator(newAdministrator);
            Administrator retrievedAdministrator = _context.Administrators.AsNoTracking().Where(a => a.Id == administratorId).FirstOrDefault();

            Assert.AreEqual(retrievedAdministrator, newAdministrator);
            Assert.AreEqual(retrievedAdministrator.Password, newAdministrator.Password);
            Assert.AreEqual(retrievedAdministrator.Email, newAdministrator.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateAdministratorWhichEmailAlreadyExists()
        {
            int administratorId = 7;
            Administrator administrator = CreateAdministratorWithSpecificId(administratorId);
            InsertAdministratorIntoDb(administrator);

            Administrator newAdministrator = new Administrator()
            {
                Id = administratorId,
                Email = administrator.Email,
                Password = "Password2"
            };

            _repository.UpdateAdministrator(newAdministrator);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void UpdateAdministratorDoesntExist()
        {
            _repository.UpdateAdministrator(new Administrator()
            {
                Id = -47,
                Email = "email@gmail.com",
                Password = "password"
            });
        }

        #region Helpers
        public void InsertAdministratorIntoDb(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
            _context.Entry(administrator).State = EntityState.Detached;
        }
        public void InsertAuthorizationTokenIntoDb(AuthorizationToken authorizationToken)
        {
            _context.Entry(authorizationToken.Administrator).State = EntityState.Unchanged;
            _context.AuthorizationTokens.Add(authorizationToken);
            _context.SaveChanges();
            _context.Entry(authorizationToken.Administrator).State = EntityState.Detached;
            _context.Entry(authorizationToken).State = EntityState.Detached;
        }
        private void LoadAdministrators(List<Administrator> administrators)
        {
            Administrator administrator1 = new Administrator() { Id = 1, Email = "email@email.com", Password = "password" };
            Administrator administrator2 = new Administrator() { Id = 2, Email = "email2@email.com", Password = "password2" };

            administrators.Add(administrator1);
            administrators.Add(administrator2);

            _context.Administrators.Add(administrator1);
            _context.Administrators.Add(administrator2);
            _context.SaveChanges();
        }
        private Administrator CreateAdministratorWithSpecificId(int administratorId)
        {
            return new Administrator()
            {
                Id = administratorId,
                Email = "admin@gmail.com",
                Password = "password"
            };
        }
        private AuthorizationToken CreateAuthorizationTokenForSpecificAdmin(Administrator administrator)
        {
            return new AuthorizationToken()
            {
                Administrator = administrator,
                ValidSince = new DateTime(2020, 10, 9)
            };
        }
        #endregion
    }
}
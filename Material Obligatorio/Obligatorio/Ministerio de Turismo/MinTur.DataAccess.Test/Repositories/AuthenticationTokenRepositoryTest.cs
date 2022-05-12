using MinTur.DataAccess.Contexts;
using MinTur.DataAccess.Repositories;
using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.EntityFrameworkCore;

namespace MinTur.DataAccess.Test.Repositories
{
    [TestClass]
    public class AuthenticationTokenRepositoryTest
    {
        private AuthenticationTokenRepository _repository;
        private NaturalUruguayContext _context;

        [TestInitialize]
        public void SetUp() 
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new AuthenticationTokenRepository(_context);
        }

        [TestCleanup]
        public void CleanUp() 
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAuthenticationTokenByIdReturnsAsExpected() 
        {
            AuthorizationToken expectedToken = CreateAuthenticationToken();
            _context.Administrators.Add(expectedToken.Administrator);
            _context.AuthorizationTokens.Add(expectedToken);
            _context.SaveChanges();

            AuthorizationToken retrievedToken = _repository.GetAuthenticationTokenById(expectedToken.Id);
            Assert.AreEqual(expectedToken, retrievedToken);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetAuthenticationTokenByIdWhichDoesntExist()
        {
            _repository.GetAuthenticationTokenById(new Guid());
        }


        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateNewAuthorizationTokenForNonExistentAdministrator()
        {
            Administrator nonExistentAdministrator = new Administrator()
            {
                Email = "nonexistent@email.com",
                Password = "ufheuhguer"
            };
            _repository.CreateNewAuthorizationTokenFor(nonExistentAdministrator);
        }

        [TestMethod]
        public void CreateNewAuthorizationTokenReturnsAsExpected()
        {
            Administrator administrator = new Administrator()
            {
                Email = "exmaple@gmail.com",
                Password = "okiofjiergji"
            };
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
            _context.Entry(administrator).State = EntityState.Detached;

            Guid newToken = _repository.CreateNewAuthorizationTokenFor(administrator);
            Assert.IsNotNull(_context.AuthorizationTokens.Find(newToken));
        }

        [TestMethod]
        public void CreateNewAuthorizationTokenDeletesOldOnes()
        {
            Administrator administrator = new Administrator()
            {
                Email = "exmaple@gmail.com",
                Password = "okiofjiergji"
            };
            AuthorizationToken oldToken = new AuthorizationToken()
            {
                Administrator = administrator
            };
            _context.Administrators.Add(administrator);
            _context.AuthorizationTokens.Add(oldToken);
            _context.SaveChanges();
            _context.Entry(administrator).State = EntityState.Detached;

            Guid newTokenId = _repository.CreateNewAuthorizationTokenFor(administrator);
            Assert.IsNotNull(_context.AuthorizationTokens.Find(newTokenId));
            Assert.IsNull(_context.AuthorizationTokens.Find(oldToken.Id));
        }

        #region Helpers
        public AuthorizationToken CreateAuthenticationToken() 
        {
            return new AuthorizationToken()
            {
                Administrator = new Administrator()
                {
                    Id = 1,
                    Email = "email@email.com",
                    Password = "password"
                }
            };
        }
        #endregion
    }
}

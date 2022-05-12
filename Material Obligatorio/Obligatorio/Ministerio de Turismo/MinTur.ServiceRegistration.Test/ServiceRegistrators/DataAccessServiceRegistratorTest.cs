using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.DataAccessInterface.Facades;
using MinTur.ServiceRegistration.ServiceRegistrators;
using MinTur.ServiceRegistration.Test.Dummies;
using System;

namespace MinTur.ServiceRegistration.Test.ServiceRegistrators
{
    [TestClass]
    public class DataAccessServiceRegistratorTest
    {
        private IServiceCollection _serviceCollection;
        private DataAccessServiceRegistrator _dataAccessServiceRegistrator;

        [TestInitialize]
        public void SetUp()
        {
            _serviceCollection = new ServiceCollection();
            _dataAccessServiceRegistrator = new DataAccessServiceRegistrator();
        }

        [TestMethod]
        public void ContextIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(DbContext));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void ContextIsRegisteredAfterCallingRegisterServiceMethod()
        {
            _dataAccessServiceRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(DbContext));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(DbContext).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void RepositoryFacadeIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IRepositoryFacade));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void RepositoryFacadeIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterRepositoryFacadeDependencies();
            _dataAccessServiceRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IRepositoryFacade));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IRepositoryFacade).IsAssignableFrom(service.GetType()));
        }

        #region Helpers
        private void RegisterRepositoryFacadeDependencies()
        {
            _serviceCollection.AddDbContext<DbContext, ContextDummy>();
        }
        #endregion
    }
}

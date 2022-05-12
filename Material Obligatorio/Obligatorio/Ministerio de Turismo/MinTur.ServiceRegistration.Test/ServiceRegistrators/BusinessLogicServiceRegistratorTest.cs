using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.ServiceRegistration.ServiceRegistrators;
using MinTur.ServiceRegistration.Test.Dummies;
using System;
using MinTur.BusinessLogicInterface.Pricing;
using MinTur.BusinessLogicInterface.Security;
using MinTur.BusinessLogicInterface.Importing;
using Microsoft.Extensions.Configuration;

namespace MinTur.ServiceRegistration.Test.ServiceRegistrators
{
    [TestClass]
    public class BusinessLogicServiceRegistratorTest
    {
        private IServiceCollection _serviceCollection;
        private BusinessLogicServiceRegistrator _businessLogicRegistrator;

        [TestInitialize]
        public void SetUp()
        {
            _serviceCollection = new ServiceCollection();
            _businessLogicRegistrator = new BusinessLogicServiceRegistrator();
        }

        [TestMethod]
        public void RegionManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IRegionManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void RegionManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterRegionManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IRegionManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IRegionManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void TouristPointManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(ITouristPointManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void TouristPointManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterTouristPointManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(ITouristPointManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(ITouristPointManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void CategoryManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(ICategoryManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void CategoryManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterCategoryManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(ICategoryManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(ICategoryManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void ResortManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IResortManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void ResortManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterCategoryManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IResortManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IResortManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void ReservationManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IReservationManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void ReservationManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterReservationManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IReservationManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IReservationManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void AdministratorManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IAdministratorManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void AdministratorManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterAdministratorManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IAdministratorManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IAdministratorManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void ResortPricingCalculatorIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IResortPricingCalculator));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void ResortPricingCalculatorIsRegisteredAfterCallingRegisterServiceMethod()
        {
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IResortPricingCalculator));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IResortPricingCalculator).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void AuthenticationManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IAuthenticationManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void AuthenticationManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterAuthenticationManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IAuthenticationManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IAuthenticationManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void ReviewManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IReviewManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void ReviewManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterAuthenticationManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IReviewManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IReviewManager).IsAssignableFrom(service.GetType()));
        }

        [TestMethod]
        public void ImportManagerIsNotRegisteredBeforeCallingRegisterServiceMethod()
        {
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IImporterManager));

            Assert.IsTrue(service == null);
        }

        [TestMethod]
        public void ImportManagerIsRegisteredAfterCallingRegisterServiceMethod()
        {
            RegisterImporterManagerDependencies();
            _businessLogicRegistrator.RegistrateServices(_serviceCollection);
            IServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();
            object service = serviceProvider.GetService(typeof(IImporterManager));

            Assert.IsTrue(service != null);
            Assert.IsTrue(typeof(IImporterManager).IsAssignableFrom(service.GetType()));
        }

        private void RegisterAuthenticationManagerDependencies()
        {
            _serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacadeDummy>();
        }

        #region Helpers
        private void RegisterImporterManagerDependencies()
        {
            _serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacadeDummy>();
            _serviceCollection.AddScoped<IConfiguration, ConfigurationDummy>();
        }
        private void RegisterRegionManagerDependencies()
        {
            _serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacadeDummy>();
        }
        private void RegisterTouristPointManagerDependencies()
        {
            _serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacadeDummy>();
            _serviceCollection.AddScoped<IResortPricingCalculator, ResortPricingCalculatorDummy>();
        }
        private void RegisterCategoryManagerDependencies()
        {
            _serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacadeDummy>();
        }

        private void RegisterReservationManagerDependencies()
        {
            _serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacadeDummy>();
            _serviceCollection.AddScoped<IResortPricingCalculator, ResortPricingCalculatorDummy>();
        }

        private void RegisterAdministratorManagerDependencies()
        {
            _serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacadeDummy>();
        }

        #endregion

    }
}

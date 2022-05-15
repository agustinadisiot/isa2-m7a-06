using Microsoft.Extensions.DependencyInjection;
using MinTur.BusinessLogic.ResourceManagers;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.ServiceRegistration.Interfaces;
using MinTur.BusinessLogicInterface.Pricing;
using MinTur.BusinessLogic.Pricing;
using MinTur.BusinessLogicInterface.Security;
using MinTur.BusinessLogic.Security;
using MinTur.BusinessLogic.Importing;
using MinTur.BusinessLogicInterface.Importing;

namespace MinTur.ServiceRegistration.ServiceRegistrators
{
    public class BusinessLogicServiceRegistrator : IServiceRegistrator
    {
        public BusinessLogicServiceRegistrator() { }

        public void RegistrateServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRegionManager, RegionManager>();
            serviceCollection.AddScoped<ITouristPointManager, TouristPointManager>();
            serviceCollection.AddScoped<IChargingPointManager, ChargingPointManager>();
            serviceCollection.AddScoped<ICategoryManager, CategoryManager>();
            serviceCollection.AddScoped<IResortManager, ResortManager>();
            serviceCollection.AddScoped<IReservationManager, ReservationManager>();
            serviceCollection.AddScoped<IReviewManager, ReviewManager>();
            serviceCollection.AddScoped<IAdministratorManager, AdministratorManager>();
            serviceCollection.AddScoped<IResortPricingCalculator, ResortPricingCalculator>();
            serviceCollection.AddScoped<IAuthenticationManager, AuthenticationManager>();
            serviceCollection.AddScoped<IImporterManager, ImporterManager>();
        }
    }
}

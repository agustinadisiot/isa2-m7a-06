using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinTur.DataAccess.Contexts;
using MinTur.DataAccess.Facades;
using MinTur.DataAccessInterface.Facades;
using MinTur.ServiceRegistration.Interfaces;

namespace MinTur.ServiceRegistration.ServiceRegistrators
{
    public class DataAccessServiceRegistrator : IServiceRegistrator
    {
        public DataAccessServiceRegistrator() { }

        public void RegistrateServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepositoryFacade, RepositoryFacade>();
            serviceCollection.AddDbContext<DbContext, NaturalUruguayContext>();
        } 
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace MinTur.ServiceRegistration.Interfaces
{
    public interface IServiceRegistrator
    {
        void RegistrateServices(IServiceCollection serviceCollection);
    }
}

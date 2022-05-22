using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinTur.ServiceRegistration.Interfaces;
using MinTur.WebApi.Filters;

namespace MinTur.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything" ,builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
            services.AddScoped<AdministratorAuthorizationFilter>();
            RegisterConfiguration(services);
            RegisterAppServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterAppServices(IServiceCollection services)
        {
            IEnumerable<Type> serviceRegistratorTypes = typeof(IServiceRegistrator).Assembly.GetTypes()
                .Where(t => typeof(IServiceRegistrator).IsAssignableFrom(t) && !t.IsInterface);

            foreach(Type serviceRegistratorType in serviceRegistratorTypes) 
            {
                IServiceRegistrator serviceRegistrator = (IServiceRegistrator)Activator.CreateInstance(serviceRegistratorType);
                serviceRegistrator.RegistrateServices(services);
            }
        }

        private void RegisterConfiguration(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            Directory.CreateDirectory(Configuration.GetSection("ImportersDllDirectory").Value);
            Directory.CreateDirectory(Configuration.GetSection("JSONFilesForImporter").Value);
            Directory.CreateDirectory(Configuration.GetSection("XMLFilesForImporter").Value);
        }
    }
}

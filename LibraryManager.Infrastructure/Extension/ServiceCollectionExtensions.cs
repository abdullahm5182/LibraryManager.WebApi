using LibraryManager.Infrastructure.Database;
using LibraryManager.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;  
using System.Reflection; 

namespace LibraryManager.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // 1. Scan entire assembly for IRegister implementations
            var assemblies = new[]
            {
    Assembly.GetExecutingAssembly()
};

            void RegisterAssemblyServices(Assembly assembly, string suffix, ServiceLifetime lifetime, string excludeName = null)
            {
                services.RegisterAssemblyPublicNonGenericClasses(assembly)
                        .Where(x => x.Name.EndsWith(suffix) && (excludeName == null || x.Name != excludeName))
                        .AsPublicImplementedInterfaces(lifetime);
            }

            // 4. Register services + repositories
            foreach (var assembly in assemblies)
            {
                RegisterAssemblyServices(assembly, "Repository", ServiceLifetime.Scoped);
            }

            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            return services;
        }
    }
}

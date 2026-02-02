using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Extension
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var config = new TypeAdapterConfig();
            config.Scan(assembly); // Scans current project
            services.AddSingleton(config);

            services.AddSingleton<IMapper, ServiceMapper>();

            void RegisterAssemblyServices(Assembly assembly, string suffix, ServiceLifetime lifetime, string excludeName = null)
            {
                services.RegisterAssemblyPublicNonGenericClasses(assembly)
                        .Where(x => x.Name.EndsWith(suffix) && (excludeName == null || x.Name != excludeName))
                        .AsPublicImplementedInterfaces(lifetime);
            }

            RegisterAssemblyServices(assembly, "Service", ServiceLifetime.Scoped);

            // Add application services registration logic here
            return services;
        } 
    
    }
}

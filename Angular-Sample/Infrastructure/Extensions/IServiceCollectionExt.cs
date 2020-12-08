using Angular_Sample.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Util.Data;

namespace Angular_Sample.Infrastructure.Extensions
{
    public static class IServiceCollectionExt
    {

        public static void AddSpa(this IServiceCollection services)
        {
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddDbContext<SecuritiesTransactionContext>(ServiceLifetime.Transient);

            services.AddScoped<IDbContextProxy, DbContextProxy>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddHttpContextAccessor();
            AddScopeFormAssembly(services, "Angular-Sample.Service");
        }

        private static void AddScopeFormAssembly(IServiceCollection services, string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            IEnumerable<Type> interfaces = GetInterfaces(assembly);
            IEnumerable<Type> types = GetClasses(assembly);

            foreach (var implType in types)
            {
                AddScopeByName(services, implType, interfaces);
            }
        }

        private static IEnumerable<Type> GetInterfaces(Assembly assembly)
        {
            return from t in assembly.GetTypes()
                   where t.IsInterface && t.IsPublic
                   select t;
        }

        private static IEnumerable<Type> GetClasses(Assembly assembly)
        {
            return from t in assembly.GetTypes()
                   where !t.IsInterface && t.IsClass && t.IsPublic
                   select t;
        }

        private static void AddScopeByName(IServiceCollection services, Type implType, IEnumerable<Type> interfaces)
        {
            Type serviceType = interfaces.SingleOrDefault(t => t.Name == "I" + implType.Name);

            if (serviceType != null)
            {
                services.AddScoped(serviceType, implType);
            }
        }
    }
}

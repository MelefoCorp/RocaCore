using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Roca.Core.Interfaces;
using Roca.Core.Translation;
using System;
using System.Linq;
using System.Reflection;

namespace Roca.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingletonInterface<T>(this IServiceCollection collection, params Assembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            var inter = typeof(T);
            var assembliesServices = assemblies.Select(x => x.GetTypes().Where(y => y.GetInterfaces().Contains(inter) && !y.IsAbstract && !y.IsInterface));

            foreach (var assembly in assembliesServices)
                foreach (var service in assembly)
                    collection.AddSingleton(service);
            return collection;
        }

        public static IServiceCollection AddTransientInterface<T>(this IServiceCollection collection, params Assembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            var inter = typeof(T);
            var assembliesServices = assemblies.Select(x => x.GetTypes().Where(y => y.GetInterfaces().Contains(inter) && !y.IsAbstract && !y.IsInterface));

            foreach (var assembly in assembliesServices)
                foreach (var service in assembly)
                    collection.AddSingleton(service);
            return collection;
        }

        public static IServiceCollection AddScopedInterface<T>(this IServiceCollection collection, params Assembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            var inter = typeof(T);
            var assembliesServices = assemblies.Select(x => x.GetTypes().Where(y => y.GetInterfaces().Contains(inter) && !y.IsAbstract && !y.IsInterface));

            foreach (var assembly in assembliesServices)
                foreach (var service in assembly)
                    collection.AddSingleton( service);
            return collection;
        }

        public static IServiceCollection AddRocaLocalization(this IServiceCollection collection)
        {
            collection.AddSingleton<IStringLocalizerFactory, RocaLocalizerFactory>();
            return collection.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        }
    }
}

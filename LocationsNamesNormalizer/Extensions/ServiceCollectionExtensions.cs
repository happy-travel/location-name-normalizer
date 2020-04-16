using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LocationNameNormalizer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNameNormalizer(this IServiceCollection services)
        {
            services.TryAddSingleton<ILocationNamesRetriever, LocationNamesRetriever>();
            services.TryAddSingleton<IDefaultLocationNamesSelector, DefaultLocationNameSelector>();
            services.TryAddSingleton<INameNormalizer, NameNormalizer>();

            return services;
        }
    }
}
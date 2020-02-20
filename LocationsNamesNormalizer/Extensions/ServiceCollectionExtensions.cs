using Microsoft.Extensions.DependencyInjection;

namespace LocationsNamesNormalizer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultLocationsNamesSelector(this IServiceCollection services)
            => services.AddSingleton<IDefaultLocationNamesSelector, DefaultLocationNameSelector>();


        public static IServiceCollection AddLocationNamesRetriever(this IServiceCollection services)
            => services.AddSingleton<ILocationNamesRetriever, LocationNamesRetriever>();


        public static IServiceCollection AddNameNormalizer(this IServiceCollection services) => services.AddSingleton<INameNormalizer, NameNormalizer>();
    }
}
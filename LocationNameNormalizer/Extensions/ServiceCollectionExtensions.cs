using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LocationNameNormalizer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNameNormalizationServices(this IServiceCollection services)
        {
            services.TryAddSingleton<ILocationNameRetriever, FileLocationNameRetriever>();
            services.TryAddSingleton<ILocationNameNormalizer>(provider =>
            {
                var retriever = provider.GetService<ILocationNameRetriever>();
                var selector = new DefaultLocationNameNormalizer(retriever);
                selector.Init();

                return selector;
            });

            return services;
        }
    }
}
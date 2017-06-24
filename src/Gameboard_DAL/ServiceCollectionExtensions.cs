using Gameboard_DAL;
using Gameboard_DAL.Entities;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NoDb;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services)
        {
            // Initialize JSON DB
            services.AddNoDb<Company>();
            services.AddNoDb<Product>();
            // Initialize DBContext
            services.TryAddScoped<DbContext>();

            return services;
        }
    }
}

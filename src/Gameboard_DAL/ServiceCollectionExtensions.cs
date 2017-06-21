using Gameboard_DAL.Repositories;
using Gameboard_DAL.Repositories.Models;
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
            // Initialize repositories
            services.TryAddScoped<ICompanyRepository, CompanyRepository>();
            services.TryAddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}

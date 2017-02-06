using Gameboard_DAL.Models;
using Gameboard_DAL.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NoDb;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services)
        {
            services.AddNoDb<Company>();
            services.AddNoDb<Product>();
            services.TryAddScoped<ICompanyRepository, CompanyRepository>();
            services.TryAddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}

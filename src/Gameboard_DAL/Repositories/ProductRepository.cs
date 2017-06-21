using Gameboard_DAL.Repositories.Models;
using Gameboard_DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NoDb;

namespace Gameboard_DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(
            IOptions<DALSettings> settings, 
            IBasicQueries<Product> query, 
            IBasicCommands<Product> commands,
            ICompanyRepository companyRepository,
            IHttpContextAccessor contextAccessor = null
            ) 
        {
            Context = new BaseRepository<Product, Product, IProduct>(settings, query, commands, contextAccessor);
        }
        
        public BaseRepository<Product, Product, IProduct> Context { get; }

    }
}
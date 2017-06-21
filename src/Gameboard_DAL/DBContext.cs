using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gameboard_DAL.Repositories;
using Gameboard_DAL.Repositories.Models;

namespace Gameboard_DAL
{
    public class DBContext
    {
        public BaseRepository Companies { get; }
        public BaseRepository Products { get; }

        public DBContext(IOptions<DALSettings> settings, IBasicQueries<Company> query, IBasicCommands<Company> commands, IHttpContextAccessor contextAccessor = null)
        {
            Companies = new BaseRepository<Company, Company, ICompany>(settings, query, commands, contextAccessor);
            Products = new BaseRepository<Product, Product, IProduct>(settings, query, commands, contextAccessor);
        }
    }
}

﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gameboard_DAL.Models;
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
            IHttpContextAccessor contextAccessor = null) 
        {
            Context = new BaseRepository<Product, Product>(settings, query, commands, contextAccessor);
        }
        
        public BaseRepository<Product, Product> Context { get; }
    }
}
using System;
using System.Linq;
using Gameboard_DAL.Repositories.Models;
using Gameboard_DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NoDb;

namespace Gameboard_DAL.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {

        protected IProductRepository ProductRepository;

        /// <summary>
        /// Just for mocking, shouldn't be used in real-world context.
        /// </summary>
        public CompanyRepository()
        {
            
        }

        public CompanyRepository(
            IOptions<DALSettings> settings, 
            IBasicQueries<Company> query,
            IBasicCommands<Company> commands,
            IProductRepository productRepository,
            IHttpContextAccessor contextAccessor = null
            )
        {
            Context = new BaseRepository<Company, Company, ICompany>(settings, query, commands, contextAccessor);
            ProductRepository = productRepository;
            Context.OnEntityDeleted += ContextOnEntityDeleted;
        }

        private void ContextOnEntityDeleted(object sender, EntityDeletedEventArgs e)
        {
            // cascade deletion of the products associated to this company.
            foreach (var product in ProductRepository.Context.GetAll().Result.Where(p => p.Company.Id == e.EntityId))
            {
                ProductRepository.Context.Delete(product.Id).ConfigureAwait(false);
            }
        }

        public BaseRepository<Company, Company, ICompany> Context { get; }
    }
}
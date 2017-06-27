using System;
using System.Linq;
using Gameboard_DAL.Entities;
using Gameboard_DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NoDb;

namespace Gameboard_DAL
{
    public class DbContext
    {
        public virtual DbRepository<Company> Companies { get; set; }
        public virtual DbRepository<Product> Products { get; }


        /// <summary>
        /// Just for mocking purpose, shouldn't be used in real-world context.
        /// </summary>
        public DbContext()
        {

        }

        public DbContext(IOptions<DALSettings> settings,
            IBasicQueries<Company> companyQueries,
            IBasicQueries<Product> productQueries,
            IBasicCommands<Company> companyCommands,
            IBasicCommands<Product> productCommands,
            IHttpContextAccessor contextAccessor = null)
        {
            Companies = new DbRepository<Company>(settings, companyQueries, companyCommands, contextAccessor);
            Products = new DbRepository<Product>(settings, productQueries, productCommands, contextAccessor);

            // register cascade deletion
            Companies.OnEntityDeleted += Companies_OnEntityDeleted;
            // load related entities
            Products.OnEntityRetrieved += Products_OnEntityRetrieved; 
        }

        private void Products_OnEntityRetrieved(object sender, EntityRetrievedEventArgs<Product> entityRetrievedEventArgs)
        {
            entityRetrievedEventArgs.EntityInstance.Company = Companies.Get(entityRetrievedEventArgs.EntityInstance.CompanyId).Result;
        }

        //#region Cascade deletion
        private async void Companies_OnEntityDeleted(object sender, EntityDeletedEventArgs e)
        {
            var productList = await Products.GetAll();
            foreach (var product in productList.Where(d => d.CompanyId.Equals(e.EntityId)))
            {
                await Products.Delete(product.Id);
            }
        }
        //#endregion
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gameboard.MetaModels;
using Gameboard_DAL;
using Gameboard_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gameboard.Controllers.Api
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly DbContext _dbContext;

        public ProductsController(
            DbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var productList = await _dbContext.Products.GetAll();
            //productList.ForEach(async d => await d.LoadCompany(_companyRepository));
            return productList;
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _dbContext.Products.Get(id);
            if (null == product)
            {
                return NotFound(id);
            }
            //await product.LoadCompany(_companyRepository);
            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductModel product)
        {
            if (null == product || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newProduct = await _dbContext.Products.Create(product);
            return Ok(newProduct);
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]ProductModel product)
        {
            if (null == await _dbContext.Products.Get(id))
            {
                return NotFound(id);
            }

            if (null == product || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = id;
            return Ok(await _dbContext.Products.Update(product));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (null == await _dbContext.Products.Get(id))
            {
                return NotFound(id);
            }
            return Ok(await _dbContext.Products.Delete(id));
        }
    }
}

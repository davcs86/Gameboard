using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gameboard_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Gameboard_DAL.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gameboard.Controllers.Api
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;

        public ProductsController(
            ICompanyRepository companyRepository,
            IProductRepository productRepository
            )
        {
            _companyRepository = companyRepository;
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var productList = await _productRepository.Context.GetAll();
            productList.ForEach(async d => await d.LoadCompany(_companyRepository));
            return productList;
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRepository.Context.Get(id);
            if (null == product)
            {
                return NotFound(id);
            }
            await product.LoadCompany(_companyRepository);
            return Ok(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newProduct = await _productRepository.Context.Create(product);
            return Ok(newProduct);
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]ProductModel product)
        {
            if (null == await _productRepository.Context.Get(id))
            {
                return NotFound(id);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = id;
            return Ok(await _productRepository.Context.Update(product));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (null == await _productRepository.Context.Get(id))
            {
                return NotFound(id);
            }
            return Ok(await _productRepository.Context.Delete(id));
        }
    }
}

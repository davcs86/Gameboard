using System.Collections.Generic;
using System.Threading.Tasks;
using Gameboard.MetaModels;
using Gameboard_DAL;
using Microsoft.AspNetCore.Mvc;
using Gameboard_DAL.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gameboard.Controllers.Api
{
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly DbContext _dbContext;

        public CompaniesController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/companies
        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return await _dbContext.Companies.GetAll();
        }

        // GET api/companies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var company = await _dbContext.Companies.Get(id);
            if (null == company)
            {
                return NotFound(id);
            }
            return Ok(company);
        }

        // POST api/companies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyModel company)
        {
            if (null == company || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCompany = await _dbContext.Companies.Create(company);
            return Ok(newCompany);
        }

        // PUT api/companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] CompanyModel company)
        {
            if (null == await _dbContext.Companies.Get(id))
            {
                return NotFound(id);
            }
            if (null == company || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            company.Id = id;
            return Ok(await _dbContext.Companies.Update(company));
        }

        // DELETE api/companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (null == await _dbContext.Companies.Get(id))
            {
                return NotFound(id);
            }
            return Ok(await _dbContext.Companies.Delete(id));
        }
    }
}
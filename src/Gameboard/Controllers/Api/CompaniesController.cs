using System.Collections.Generic;
using System.Threading.Tasks;
using Gameboard.MetaModels;
using Microsoft.AspNetCore.Mvc;
using Gameboard_DAL.Repositories;
using Gameboard_DAL.Repositories.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gameboard.Controllers.Api
{
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        // GET: api/companies
        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return await _companyRepository.Context.GetAll();
        }

        // GET api/companies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var company = await _companyRepository.Context.Get(id);
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
            var newCompany = await _companyRepository.Context.Create(company);
            return Ok(newCompany);
        }

        // PUT api/companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] CompanyModel company)
        {
            if (null == await _companyRepository.Context.Get(id))
            {
                return NotFound(id);
            }
            if (null == company || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            company.Id = id;
            return Ok(await _companyRepository.Context.Update(company));
        }

        // DELETE api/companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (null == await _companyRepository.Context.Get(id))
            {
                return NotFound(id);
            }
            return Ok(await _companyRepository.Context.Delete(id));
        }
    }
}
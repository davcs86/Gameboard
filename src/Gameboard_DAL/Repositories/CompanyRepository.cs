using Gameboard_DAL.Repositories.Models;
using Gameboard_DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NoDb;

namespace Gameboard_DAL.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {

        public CompanyRepository(
            IOptions<DALSettings> settings, 
            IBasicQueries<Company> query,
            IBasicCommands<Company> commands, 
            IHttpContextAccessor contextAccessor = null)
        {
            Context = new BaseRepository<Company, Company>(settings, query, commands, contextAccessor);
        }

        public BaseRepository<Company, Company> Context { get; }
    }
}
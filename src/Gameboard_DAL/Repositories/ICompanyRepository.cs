using Gameboard_DAL.Repositories.Models;

namespace Gameboard_DAL.Repositories
{
    public interface ICompanyRepository
    {
        BaseRepository<Company, Company, ICompany> Context { get; }
    }
}
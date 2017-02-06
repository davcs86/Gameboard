using Gameboard_DAL.Models;

namespace Gameboard_DAL.Repositories
{
    public interface ICompanyRepository
    {
        BaseRepository<Company, Company> Context { get; }
    }
}
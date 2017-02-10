using Gameboard_DAL.Repositories.Models;

namespace Gameboard_DAL.Repositories
{
    public interface IProductRepository
    {
        BaseRepository<Product, Product> Context { get; }
    }
}
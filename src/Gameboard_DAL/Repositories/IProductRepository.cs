using Gameboard_DAL.Models;

namespace Gameboard_DAL.Repositories
{
    public interface IProductRepository
    {
        BaseRepository<Product, Product> Context { get; }
    }
}
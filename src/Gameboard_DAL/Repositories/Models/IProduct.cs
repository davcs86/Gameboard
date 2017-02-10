namespace Gameboard_DAL.Repositories.Models
{
    public interface IProduct: IBaseItem
    {
        string Description { get; set; }
        int AgeRestriction { get; set; }
        string CompanyId { get; set; }
        decimal Price { get; set; }
    }
}
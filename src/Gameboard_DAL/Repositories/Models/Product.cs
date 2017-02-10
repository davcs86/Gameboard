using System;
using System.Threading.Tasks;

namespace Gameboard_DAL.Repositories.Models
{
    public class Product: IProduct
    {
        public void FromInterface(IBaseItem item)
        {
            var product = item as IProduct;
            if (product == null) return;
            Id = product.Id ?? Guid.NewGuid().ToString();
            CreationTime = product.CreationTime ?? DateTime.UtcNow;
            LastModified = product.LastModified ?? DateTime.UtcNow;
            Name = product.Name;
            Description = product.Description;
            AgeRestriction = product.AgeRestriction;
            CompanyId = product.CompanyId;
            Price = product.Price;
        }

        public async Task<Product> LoadCompany(ICompanyRepository companyRepository)
        {
            if (!string.IsNullOrWhiteSpace(CompanyId))
            {
                Company = await companyRepository.Context.Get(CompanyId);
            }
            return this;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AgeRestriction { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? LastModified { get; set; }

    }
}

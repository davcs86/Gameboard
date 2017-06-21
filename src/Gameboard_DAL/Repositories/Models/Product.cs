using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Gameboard_DAL.Repositories.Models
{
    public class Product: IProduct
    {
        private static ICompanyRepository _companyRepository;

        public void FromInterface(IBaseItem item)
        {
            FromInterface(item as IProduct);
        }

        public void FromInterface(IProduct item)
        {
            var product = item;
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

        // lazy loading company
        private Company _company;

        public Company Company
        {
            get
            {
                _company = _company ?? _companyRepository.Context.Get(CompanyId).Result;
                return _company;
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AgeRestriction { get; set; }
        public string CompanyId { get; set; }
        
        public decimal Price { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? LastModified { get; set; }

    }
}

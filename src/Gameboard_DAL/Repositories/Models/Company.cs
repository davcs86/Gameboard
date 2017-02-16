using System;

namespace Gameboard_DAL.Repositories.Models
{
    public class Company : ICompany
    {
        public DateTime? LastModified { get; set; }

        public void FromInterface(ICompany item)
        {
            var company = item;
            if (company == null) return;
            Id = company.Id ?? Guid.NewGuid().ToString();
            CreationTime = company.CreationTime ?? DateTime.UtcNow;
            LastModified = company.LastModified ?? DateTime.UtcNow;
            Name = company.Name;
        }

        public void FromInterface(IBaseItem item)
        {
            FromInterface(item as ICompany);
        }

        public string Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Name { get; set; }
    }
}
using System;

namespace Gameboard_DAL.Models
{
    public class Company : ICompany
    {
        public DateTime? LastModified { get; set; }

        public void FromInterface(IBaseItem item)
        {
            var company = item as ICompany;
            if (company == null) return;
            Id = company.Id ?? Guid.NewGuid().ToString();
            CreationTime = company.CreationTime ?? DateTime.UtcNow;
            LastModified = company.LastModified ?? DateTime.UtcNow;
            Name = company.Name;
        }

        public string Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Name { get; set; }
    }
}
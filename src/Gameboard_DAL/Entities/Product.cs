using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace Gameboard_DAL.Entities
{
    public partial class Product : BaseItem
    {
        public Company Company { get; set; }

        public string Description { get; set; }
        public int AgeRestriction { get; set; }
        public string CompanyId { get; set; }
        public decimal Price { get; set; }
    }
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gameboard_DAL.Repositories.Models;

namespace Gameboard.MetaModels
{
    public class ProductModel : IProduct
    {
        public DateTime? CreationTime { get; set; }

        public string Id { get; set; }

        public DateTime? LastModified { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [DisplayName("Name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name can have between 5 and 50 characters, inclusive.")]
        public string Name { get; set; }

        //Description, Yes, Max length 500
        [DisplayName("Description")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [DisplayName("Age restriction")]
        [Range(0, 100, ErrorMessage = "Age restriction must be between 0 to 100 years, inclusive.")]
        //AgeRestriction, Yes, 0 to 100
        public int AgeRestriction { get; set; }

        [DisplayName("Company")]
        [StringLength(36, MinimumLength = 1, ErrorMessage = "Please, select a company.")]
        public string CompanyId { get; set; }

        [DisplayName("Price")]
        [Range(1, 1000, ErrorMessage = "Price must be between $ 1.00 and $ 1000.00, inclusive.")]
        public decimal Price { get; set; }

        public void FromInterface(IBaseItem item)
        {
            throw new NotImplementedException();
        }
        
    }
}

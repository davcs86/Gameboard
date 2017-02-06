using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Gameboard_DAL.Models
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public class ProductModel : Product
    {  
    }

    public class ProductMetadata
    {

        [Required(ErrorMessage = "Name is required.")]
        [DisplayName("Name")]
        [StringLength(50, ErrorMessage = "Name cannot be larger than 50 characters.")]
        public string Name { get; set; }

        //Description, Yes, Max length 100
        [DisplayName("Description")]
        [StringLength(100, ErrorMessage = "Description cannot be larger than 100 characters.")]
        string Description { get; set; }

        [DisplayName("Age restriction")]
        [Range(0, 100, ErrorMessage = "Age restriction must be between 0 to 100 years, inclusive.")]
        //AgeRestriction, Yes, 0 to 100
        int AgeRestriction { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        string CompanyId { get; set; }

        [DisplayName("Price")]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 to 1000 years, inclusive.")]
        decimal Price { get; set; }
    }
}

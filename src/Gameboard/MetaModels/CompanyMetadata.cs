using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Gameboard_DAL.Repositories.Models;

namespace Gameboard.MetaModels
{
    [ModelMetadataType(typeof(CompanyMetadata))]
    public class CompanyModel : Company
    {  
    }

    public class CompanyMetadata
    {

        [Required(ErrorMessage = "Name is required.")]
        [DisplayName("Name")]
        [MinLength(5, ErrorMessage = "Name must have at least 5 characters.")]
        [StringLength(50, ErrorMessage = "Name cannot be larger than 50 characters.")]
        public string Name { get; set; }
    }
}

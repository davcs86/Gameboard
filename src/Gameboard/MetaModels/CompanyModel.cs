using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gameboard_DAL.Entities;

namespace Gameboard.MetaModels
{
    public class CompanyModel: Company
    {
        [Required(ErrorMessage = "Name is required.")]
        [DisplayName("Name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name can have between 5 and 50 characters, inclusive.")]
        public new string Name { get; set; }

    }
}

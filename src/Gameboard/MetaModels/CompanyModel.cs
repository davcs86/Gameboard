using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gameboard_DAL.Repositories.Models;

namespace Gameboard.MetaModels
{
    public class CompanyModel: ICompany
    {
        public DateTime? CreationTime { get; set; }

        public string Id { get; set; }

        public DateTime? LastModified { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [DisplayName("Name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name can have between 5 and 50 characters, inclusive.")]
        public string Name { get; set; }

        public void FromInterface(IBaseItem item)
        {
            throw new NotImplementedException();
        }
    }
}

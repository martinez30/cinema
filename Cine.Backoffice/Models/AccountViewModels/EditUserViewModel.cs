using Infra;
using System.ComponentModel.DataAnnotations;

namespace Cine.Backoffice.Models.AccountViewModels
{
    public class EditUserViewModel
    {

        [Required(ErrorMessage=Messages.RequiredField)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage=Messages.RequiredField)]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = Messages.InvalidEmail)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
    }
}

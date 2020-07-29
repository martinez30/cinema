using Infra;
using System.ComponentModel.DataAnnotations;

namespace Cine.Backoffice.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage=Messages.RequiredField)]
        [EmailAddress(ErrorMessage = Messages.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage=Messages.RequiredField)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; internal set; }
    }
}

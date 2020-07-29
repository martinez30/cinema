using Infra;
using System.ComponentModel.DataAnnotations;


namespace Cine.Backoffice.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage=Messages.RequiredField)]
        [EmailAddress(ErrorMessage = Messages.InvalidEmail)]
        public string Email { get; set; }
    }
}

using Infra;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Cine.Backoffice.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = Messages.RequiredField)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = Messages.RequiredField)]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        [EmailAddress(ErrorMessage = Messages.InvalidEmail)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        [StringLength(100, ErrorMessage = "A senha atual deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não conferem.")]
        public string ConfirmPassword { get; set; }

       
    }
}

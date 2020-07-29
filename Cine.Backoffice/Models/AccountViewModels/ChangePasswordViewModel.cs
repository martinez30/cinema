using Infra;
using System.ComponentModel.DataAnnotations;

namespace Cine.Backoffice.Models.AccountViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Senha atual")]
        [Required(ErrorMessage = Messages.RequiredField)]
        [StringLength(100, ErrorMessage = "A senha atual deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Nova senha")]
        [Required(ErrorMessage = Messages.RequiredField)]
        [StringLength(100, ErrorMessage = "A nova senha deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmação de senha")]
        [Required(ErrorMessage = Messages.RequiredField)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "A senha e a confirmação não conferem.")]
        public string ConfirmPassword { get; set; }
    }
}

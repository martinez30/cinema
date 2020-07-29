﻿using Infra;
using System.ComponentModel.DataAnnotations;

namespace Cine.Backoffice.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = Messages.RequiredField)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Nova senha")]
        [Required(ErrorMessage = Messages.RequiredField)]
        [StringLength(100, ErrorMessage = "A senha deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não conferem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        public string RecaptchaSiteKey { get; set; }
    }
}

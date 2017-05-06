using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EuroJobsCrm.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обязательное!")]
        [EmailAddress(ErrorMessage = "Не корректный адрес Email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательное!")]
        [StringLength(100, ErrorMessage = "Пароль должен быть минимум 6 знаков", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Минимум 6 знаков включая прописные буквы, цифры и специальные символы!" ) ]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}

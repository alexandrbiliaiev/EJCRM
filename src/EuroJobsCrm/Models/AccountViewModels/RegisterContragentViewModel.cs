using System.ComponentModel.DataAnnotations;

namespace EuroJobsCrm.Models.AccountViewModels
{
    public class RegisterContragentViewModel : RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обязательное!")]
        [Display(Name = "Name")]

        public string Name { get; set; }

        [Required (ErrorMessage = "Поле обязательное!"), 
            RegularExpression("^[^А-Яа-яёЁ]+$", ErrorMessage = "Только латинские буквы!")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Поле обязательное!")]
        public bool PersonalDataUse { get; set; }
    }
}
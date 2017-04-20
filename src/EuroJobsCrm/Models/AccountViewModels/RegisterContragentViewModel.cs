using System.ComponentModel.DataAnnotations;

namespace EuroJobsCrm.Models.AccountViewModels
{
    public class RegisterContragentViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]

        public string Name { get; set; }

        [Required, RegularExpression(@"^[A-Za-z0-9 ]+$")]
        public string CompanyName { get; set; }

        [Required]

        public bool PersonalDataUse { get; set; }
    }
}